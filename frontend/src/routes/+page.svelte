<script lang="ts">
	import FriendList from '$lib/components/FriendList.svelte';
	import type { PageProps } from './$types';
	import type { LovedOne } from '$lib/types/types';
	import { getLovedOneState } from '$lib/stores/LovedOneStore.svelte';
	import AddLovedOne from '$lib/components/AddLovedOne.svelte';

	const lovedOneStore = getLovedOneState();

	let { data }: PageProps = $props();
	const lovedOnes: LovedOne[] = lovedOneStore.addLovedOnes(data.lovedOnes);
</script>

<main class="friend-list">
	<AddLovedOne></AddLovedOne>
	<FriendList {lovedOnes}></FriendList>
</main>
