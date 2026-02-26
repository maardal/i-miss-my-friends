<script lang="ts">
	import Footer from '$lib/components/Footer.svelte';
	import FriendList from '$lib/components/FriendList.svelte';
	import Header from '$lib/components/Header.svelte';
	import type { PageProps } from './$types';
	import type { LovedOne } from '$lib/types/types';
	import { getLovedOneState } from '$lib/stores/LovedOneStore.svelte';

	const lovedOneStore = getLovedOneState();

	let { data }: PageProps = $props();
	const lovedOnes: LovedOne[] = lovedOneStore.addLovedOnes(data.lovedOnes);
</script>

<main class="layout">
	<Header></Header>
	<main class="friend-list">
		<FriendList {lovedOnes}></FriendList>
	</main>
	<Footer></Footer>
</main>
