<script lang="ts">
	import Footer from '$lib/components/Footer.svelte';
	import FriendList from '$lib/components/FriendList.svelte';
	import Header from '$lib/components/Header.svelte';
	import type { PageProps } from './$types';

	let { data }: PageProps = $props();
	let { lovedones } = data;
</script>

<main class="layout">
	<Header></Header>
	{#if lovedones.length != 0}
		<main class="friend-list">
			<FriendList {lovedones}></FriendList>
		</main>
	{:else}
		<p>No loved ones loaded</p>
	{/if}
	<Footer></Footer>
</main>
