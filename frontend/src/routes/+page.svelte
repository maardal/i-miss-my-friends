<script lang="ts">
	import Footer from '$lib/components/Footer.svelte';
	import FriendList from '$lib/components/FriendList.svelte';
	import Header from '$lib/components/Header.svelte';
	import type { PageProps } from './$types';

	let { data: lovedones }: PageProps = $props();
</script>

<main class="layout">
	<Header></Header>
	<main class="friend-list">
		<FriendList {lovedones}></FriendList>
	</main>
	<Footer></Footer>
</main>
